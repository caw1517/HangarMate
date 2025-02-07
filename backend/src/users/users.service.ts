import { Injectable } from '@nestjs/common';
import { PrismaService } from '../prisma/prisma.service';

@Injectable()
export class UsersService {
  constructor(private prisma: PrismaService) {}

  async findUserByEmail(email: string) {
    return this.prisma.user.findUnique({
      where: {
        email: email,
      },
    });
  }

  async findUserById(id: string) {
    return this.prisma.user.findUnique({
      where: {
        id,
      },
    });
  }

  async updateRefreshToken(userId: string, refreshToken: string) {
    return this.prisma.user.update({
      where: {
        id: userId,
      },
      data: {
        refreshToken: refreshToken,
      },
    });
  }

  async logOut(email: string) {
    return this.prisma.user.update({
      where: {
        email,
      },
      data: {
        refreshToken: null,
      },
    });
  }

  async GetUserCompanies(userId: string) {
    const user = await this.prisma.user.findUnique({
      where: {
        id: userId,
      },
      select: {
        company: true,
      },
    });

    console.log(user);
    if (!user || user.company == null) {
      return null;
    }

    return user.company;
  }
}
