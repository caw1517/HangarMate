import { Module } from '@nestjs/common';
import { ToolModule } from './tool/tool.module';
import { PrismaModule } from './prisma/prisma.module';
import {AuthModule} from "./auth/auth.module";
import {ConfigModule} from "@nestjs/config";
import { UsersModule } from './users/users.module';

@Module({
  imports: [
    ConfigModule.forRoot({isGlobal: true}),
    ToolModule,
    PrismaModule,
    AuthModule,
    UsersModule,
  ],
})
export class AppModule {}
