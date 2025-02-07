import { IsNotEmpty } from 'class-validator';

export class CompanyDto {
  @IsNotEmpty()
  companyName: string;
}
