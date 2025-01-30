import {
  ForbiddenException,
  Injectable,
  UnauthorizedException,
} from '@nestjs/common';
import { PrismaService } from '../prisma/prisma.service';
import { AuthDto } from './dto';
import * as argon from 'argon2';
import { PrismaClientKnownRequestError } from '@prisma/client/runtime/library';
import { JwtService } from '@nestjs/jwt';
import { ConfigService } from '@nestjs/config';
import { UsersService } from '../users/users.service';

@Injectable()
export class AuthService {
  constructor(
    private prisma: PrismaService,
    private jwt: JwtService,
    private config: ConfigService,
    private userService: UsersService,
  ) {}

  async SignUp(dto: AuthDto) {
    const hashedPassword = await argon.hash(dto.password);

    try {
      const user = await this.prisma.user.create({
        data: {
          email: dto.email,
          hashedPassword,
          firstName: dto.firstName,
          lastName: dto.lastName,
        },
        select: {
          id: true,
          email: true,
          firstName: true,
          lastName: true,
        },
      });

      //Change this to a redirect later
      return this.SignJwtToken(user.id, user.email);
    } catch (error) {
      if (error instanceof PrismaClientKnownRequestError) {
        if (error.code === 'P2002')
          throw new ForbiddenException('Email already exists.');
      }
      throw error;
    }
  }

  async Login(email: string, password: string) {
    const user = await this.userService.findUserByEmail(email);

    if (user) {
      const pwMatches: boolean = await argon.verify(
        user.hashedPassword,
        password,
      );

      if (pwMatches) {
        const accessToken = await this.SignJwtToken(user.id, user.email);
        const refreshToken = await this.SignRefreshToken(user.id, user.email);

        await this.SaveRefreshToken(user.id, refreshToken);

        return { accessToken, refreshToken };
      }
    }
  }

  async Logout(email: string) {
    return this.userService.logOut(email);
  }

  async SignJwtToken(userId: string, email: string): Promise<string> {
    const payload = {
      sub: userId,
      email,
    };

    return await this.jwt.signAsync(payload, {
      expiresIn: '15m',
      secret: this.config.get<string>('JWT_SECRET'),
    });
  }

  async SignRefreshToken(userId: string, email: string): Promise<string> {
    const payload = {
      sub: userId,
      email,
    };

    return await this.jwt.signAsync(payload, {
      expiresIn: '7d',
      secret: this.config.get<string>('REFRESH_SECRET'),
    });
  }

  async RefreshToken(userId: string, refreshToken: string) {
    const user = await this.userService.findUserById(userId);

    if (!user || !user.refreshToken)
      throw new UnauthorizedException('Not authorized');

    const tokenVerified = await argon.verify(user.refreshToken, refreshToken);

    if (!tokenVerified) throw new UnauthorizedException('Not authorized');

    const accessToken = await this.SignJwtToken(user.id, user.email);
    const newRefreshToken = await this.SignRefreshToken(user.id, user.email);

    await this.SaveRefreshToken(user.id, refreshToken);

    return {
      accessToken,
      refreshToken: newRefreshToken,
    };
  }

  async SaveRefreshToken(userId: string, refreshToken: string): Promise<void> {
    const hashedToken = await argon.hash(refreshToken);

    await this.userService.updateRefreshToken(userId, hashedToken);
  }
}
