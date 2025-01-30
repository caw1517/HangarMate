import { Request } from 'express';

export interface AuthRequest extends Request {
  user?: {
    email: string;
    sub: string;
    iat: number;
    exp: number;
  };
}
