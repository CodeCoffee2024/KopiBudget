export class ApiResult<T> {
	statusCode: number;
	isSuccess: boolean;
	data: T | null;
	errors: [];
	error: [];
}
export class NullApiResult {
	statusCode: number;
	isSuccess: boolean;
	errors: [];
	error: [];
}
export class GenericListingResult<T> {
	data: T;
	pageNumber: number;
	pageSize: number;
	totalCount: number;
	totalPages: number;
}
