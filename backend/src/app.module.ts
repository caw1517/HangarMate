import { Module } from '@nestjs/common';
import { ToolModule } from './tool/tool.module';
import { PrismaModule } from './prisma/prisma.module';
import { AuthModule } from './auth/auth.module';
import { ConfigModule } from '@nestjs/config';
import { UsersModule } from './users/users.module';
import { CompanyController } from './company/company.controller';
import { CompanyService } from './company/company.service';
import { CompanyModule } from './company/company.module';

@Module({
  imports: [
    ConfigModule.forRoot({ isGlobal: true }),
    ToolModule,
    PrismaModule,
    AuthModule,
    UsersModule,
    CompanyModule,
  ],
  controllers: [CompanyController],
  providers: [CompanyService],
})
export class AppModule {}
