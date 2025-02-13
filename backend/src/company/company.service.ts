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
    userToAddEmail: string,
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
    if (!companyVerify || companyVerify.id != companyId) {
      if (requestingUser.role != 'ADMIN')
        throw new HttpException('Invalid privlidges', HttpStatus.BAD_REQUEST);

      throw new HttpException(
        'User does not have permission to add',
        HttpStatus.BAD_REQUEST,
      );
    }

    //Ensure user being added isn't already a part of another company
    const userToAdd = await this.userService.findUserByEmail(userToAddEmail);

    if (!userToAdd)
      throw new HttpException('User does not exist', HttpStatus.BAD_REQUEST);

    const newUsersCompanies = await this.userService.GetUserCompanies(
      userToAdd.id,
    );

    if (newUsersCompanies)
      throw new HttpException(
        'User already apart of another company',
        HttpStatus.BAD_REQUEST,
      );

    return this.prismaService.company.update({
      where: {
        id: companyId,
      },
      data: {
        users: {
          connect: {
            email: userToAddEmail,
          },
        },
      },
    });
  }
}
