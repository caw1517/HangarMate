import { Controller, Get, Req, UseGuards } from '@nestjs/common';
import { ToolService } from './tool.service';
import { JwtAuthGuard } from '../auth/gaurds/JwtAuthGuard';
import { Request } from 'express';

@Controller('tools')
export class ToolController {
  constructor(private readonly toolService: ToolService) {}

  @UseGuards(JwtAuthGuard)
  @Get()
  GetAllTools(@Req() req: Request) {
    console.log(req.user);
    return this.toolService.getTools();
  }
}
