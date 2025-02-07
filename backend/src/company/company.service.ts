import { Injectable } from '@nestjs/common';
import { PrismaService } from '../prisma/prisma.service';
import { CompanyDto } from './dto';
import { UsersService } from '../users/users.service';

@Injectable()
export class CompanyService {
  constructor(
    private prismaService: PrismaService,
    private userService: UsersService,
  ) {}

  async CreateCompany(dto: CompanyDto, userId: string) {
    console.log(dto.companyName, userId);
    //Get the user from the request
    const user = await this.userService.findUserById(userId);

    if (user) {
      const newCompany = await this.prismaService.company.create({
        data: {
          companyName: dto.companyName,
          users: {
            connect: {
              id: userId,
            },
          },
        },
      });
      return newCompany;
    }
  }
}
