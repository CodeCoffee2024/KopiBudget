import { GenericListingResult } from "../../domain/models/api-result";


export function mapItemsToGenericListing<T>(source: {
	items: T;
	pageNumber: number;
	pageSize: number;
	totalCount: number;
	totalPages: number;
}): GenericListingResult<T> {
	return {
		data: source.items,
		pageNumber: source.pageNumber,
		pageSize: source.pageSize,
		totalCount: source.totalCount,
		totalPages: source.totalPages,
	};
}
