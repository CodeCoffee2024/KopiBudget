import { UserFragment } from "./user";

export abstract class AuditDto {
    createdBy: UserFragment;
    updatedBy: UserFragment;
    id: string;
}
