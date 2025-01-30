import {
  Body,
  Controller, Get,
  Post,
  Req, UnauthorizedException,
  UseGuards,
} from '@nestjs/common';
import { AuthService } from './auth.service';
import { AuthDto } from './dto';
import { LocalAuthGuard } from './gaurds/LocalAuthGuard';
import { Request } from 'express';
import { JwtAuthGuard } from './gaurds/JwtAuthGuard';
import { AuthRequest } from './interfaces/user.interface';
import { RefreshTokenGuard } from './gaurds/refreshToken.guard';

@Controller('auth')
export class AuthController {
  constructor(private readonly authService: AuthService) {}

  @Post('signup')
  SignUp(@Body() dto: AuthDto) {
    return this.authService.SignUp(dto);
  }

  @UseGuards(LocalAuthGuard)
  @Post('login')
  Login(@Req() req: Request) {
    return req.user;
  }

  @UseGuards(JwtAuthGuard)
  @Get('logout')
  async Logout(@Req() req: AuthRequest) {
    if (!req.user) throw new UnauthorizedException('Not authorized');
    await this.authService.Logout(req.user.email);
  }

  @UseGuards(RefreshTokenGuard)
  @Post('refresh')
  RefreshTokens(@Req() req: AuthRequest) {
    if (!req.user) throw new UnauthorizedException('Not authorized');

    const userId = req.user.sub;
    const email = req.user.email;
    return this.authService.RefreshToken(userId, email);
  }
}
