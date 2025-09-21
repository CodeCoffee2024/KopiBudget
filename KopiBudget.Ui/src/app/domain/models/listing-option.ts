export abstract class GenericListingOption {
  search: string = '';
  // createdBy: UserFragment;
  // modifiedBy: UserFragment;
  pageNumber = 1;
  orderBy: string;
  status: string = '';
  sortDirection: string;
}
export class GenericDropdownListingOption {
  search: string = '';
  pageNumber = 1;
  orderBy: string;
  exclude: string;
  status: string = '';
  sortDirection: string;
}
