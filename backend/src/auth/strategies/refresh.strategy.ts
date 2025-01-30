import { Injectable } from '@nestjs/common';
import { PassportStrategy } from '@nestjs/passport';
import { ExtractJwt, Strategy } from 'passport-jwt';
import { ConfigService } from '@nestjs/config';
import { Request } from 'express';

@Injectable()
export class RefreshTokenStrategy extends PassportStrategy(
  Strategy,
  'jwt-refresh',
) {
  constructor(
    config: ConfigService
  ) {
    super({
      jwtFromRequest: ExtractJwt.fromAuthHeaderAsBearerToken(),
      secretOrKey: config.get<string>('REFRESH_SECRET') || '',
      passReqToCallback: true,
    });
  }

  validate(payload: any, req: Request): any {
    if (req) {
      const authHeader = req.get('Authorization') ?? ''; // Ensure it's never undefined
      const refToken: string = authHeader.replace('Bearer ', '').trim();

      console.log(...payload, refToken);
      return { ...payload, refToken };
    }
    return payload;
  }
}
