import { HttpException, HttpStatus, Injectable } from '@nestjs/common';
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

  async AddUserToCompany(
    userEmail: string,
    companyId: string,
    currentUserId: string,
  ) {
    const companyVerify =
      await this.userService.GetUserCompanies(currentUserId);

    const requestingUser = await this.userService.findUserById(currentUserId);
    if (!requestingUser)
      throw new HttpException(
        'User requesting not found',
        HttpStatus.NOT_FOUND,
      );

    //If user adding new user isn't a part of the company, disregard request
    if (companyVerify && companyVerify.id != companyId) {
      //if(requestingUser.role != 'ADMIN') //throw new HttpException()
      throw new HttpException(
        'User does not have permission to add',
        HttpStatus.BAD_REQUEST,
      );
    }
  }
}
