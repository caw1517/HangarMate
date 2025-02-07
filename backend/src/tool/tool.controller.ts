import { Controller, Get, UseGuards } from '@nestjs/common';
import { ToolService } from './tool.service';
import { JwtAuthGuard } from '../auth/gaurds/JwtAuthGuard';

@Controller('tools')
export class ToolController {
  constructor(private readonly toolService: ToolService) {}

  @UseGuards(JwtAuthGuard)
  @Get()
  GetAllTools() {
    return this.toolService.getTools();
  }
}
