import {Injectable, UnauthorizedException} from "@nestjs/common";
import {PassportStrategy} from "@nestjs/passport";
import {AuthService} from "../auth.service";
import {Strategy} from "passport-local";

@Injectable()
export class LocalStrategy extends PassportStrategy(Strategy) {
    constructor( private authService: AuthService) {
        super({usernameField: 'email'});
    }

    async validate(email: string, password: string): Promise<any> {
        const user = await this.authService.Login(email, password);

        if(!user) throw new UnauthorizedException('Invalid credentials');

        return user;
    }
}