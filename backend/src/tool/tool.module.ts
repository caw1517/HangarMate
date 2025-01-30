import { Module } from '@nestjs/common';
import { ToolService } from './tool.service';
import { ToolController } from './tool.controller';
import {AuthService} from "../auth/auth.service";
import {AuthModule} from "../auth/auth.module";

@Module({
  imports: [AuthModule],
  controllers: [ToolController],
  providers: [ToolService],
})
export class ToolModule {}
