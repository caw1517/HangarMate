import { IsNotEmpty } from 'class-validator';

export class CompanyDto {
  @IsNotEmpty()
  companyName: string;
}

export class addUserDto {
  @IsNotEmpty()
  userToAddEmail: string;

  @IsNotEmpty()
  companyId: string;
}
