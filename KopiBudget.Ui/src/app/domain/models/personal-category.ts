import { AuditDto } from "./audit";
import { GenericDropdownListingOption } from "./listing-option";

export interface PersonalCategoryFragment {
    id: string;
    name: string;
    icon: string;
    color: string;
}
export interface PersonalCategoryDto extends AuditDto {
    name: string;
    icon: string;
    color: string;
}

export class PersonalCategoryDropdownListingOption extends GenericDropdownListingOption {
    budgetId: string;
    budgetIds: string;
}
