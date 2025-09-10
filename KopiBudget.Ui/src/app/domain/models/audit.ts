import { UserFragment } from "./user";

export abstract class AuditDto {
    createdBy: UserFragment;
    createdDate: Date;
    updatedBy: UserFragment;
    id: string;
}
