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

    const isPartCompany = await this.userService.GetUserCompanies(userId);
    if (isPartCompany != null) {
      throw new Error('User is already in a company.');
    }

    if (user) {
      try {
        return this.prismaService.company.create({
          data: {
            companyName: dto.companyName,
            users: {
              connect: {
                id: userId,
              },
            },
          },
        });
      } catch (error) {
        throw new Error((error as Error).message);
      }
    }
  }
}
