import {Controller, Get, UseGuards} from '@nestjs/common';
import { ToolService } from './tool.service';
import {AuthService} from "../auth/auth.service";
import {JwtAuthGuard} from "../auth/gaurds/JwtAuthGuard";

@Controller('tools')
export class ToolController {
  constructor(private readonly toolService: ToolService, private authService: AuthService) {}

  @UseGuards(JwtAuthGuard)
  @Get()
  async getAllTools() {
    return this.toolService.getTools();
  }
}
