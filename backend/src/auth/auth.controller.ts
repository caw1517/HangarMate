import { Body, Controller, Post, Req, UseGuards } from '@nestjs/common';
import { AuthService } from './auth.service';
import { AuthDto } from './dto';
import { LocalAuthGuard } from './gaurds/LocalAuthGuard';
import { Request } from 'express';

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
}
