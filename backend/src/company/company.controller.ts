import { Body, Controller, Get, Post, Req, UseGuards } from '@nestjs/common';
import { JwtAuthGuard } from '../auth/gaurds/JwtAuthGuard';
import { CompanyService } from './company.service';
import { CompanyDto } from './dto';
import { AuthRequest } from '../auth/interfaces/user.interface';

@Controller('companies')
export class CompanyController {
  constructor(private companyService: CompanyService) {}

  @UseGuards(JwtAuthGuard)
  @Get()
  GetAllCompanies(): string {
    return 'Returns all Companies';
  }

  //Get the company a user is in

  //Create a new company
  @UseGuards(JwtAuthGuard)
  @Post()
  async CreateNewCompany(@Body() dto: CompanyDto, @Req() req: AuthRequest) {
    if (req.user) {
      await this.companyService.CreateCompany(dto, req.user.sub);
    }

    return 'Successfully Created Company but change this';
  }

  //Update a company

  //Delete a company

  //Add a user to company
}
