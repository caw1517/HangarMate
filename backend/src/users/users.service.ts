import { Injectable } from '@nestjs/common';
import { PrismaService } from '../prisma/prisma.service';

@Injectable()
export class UsersService {
  constructor(private prisma: PrismaService) {}

  async findUser(email: string) {
    return this.prisma.user.findUnique({
      where: {
        email: email,
      },
    });
  }
}
