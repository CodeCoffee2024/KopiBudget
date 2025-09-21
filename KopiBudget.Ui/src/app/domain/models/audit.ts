import { UserFragment } from './user';

export abstract class AuditDto {
  createdBy: UserFragment;
  createdOn: Date;
  updatedOn: Date;
  updatedBy: UserFragment;
  id: string;
}
