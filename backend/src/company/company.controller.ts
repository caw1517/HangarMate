import {
  Body,
  Controller,
  Get,
  HttpException,
  HttpStatus,
  Post,
  Req,
  UseGuards,
} from '@nestjs/common';
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

  //Create a new company
  @UseGuards(JwtAuthGuard)
  @Post()
  async CreateNewCompany(@Body() dto: CompanyDto, @Req() req: AuthRequest) {
    if (!req.user)
      throw new HttpException('User Not Found', HttpStatus.NOT_FOUND);

    try {
      const company = await this.companyService.CreateCompany(
        dto,
        req.user.sub,
      );

      return {
        status: HttpStatus.CREATED,
        message: `Company created successfully.`,
        data: company,
      };
    } catch (error) {
      throw new HttpException(
        (error as Error).message,
        HttpStatus.INTERNAL_SERVER_ERROR,
      );
    }
  }

  //Update a company

  //Delete a company

  //Add a user to company
}
