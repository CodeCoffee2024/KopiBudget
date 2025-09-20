import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { ApiResult, GenericListingResult, NullApiResult } from '../../domain/models/api-result';
import { PersonalCategoryDto, PersonalCategoryFragment } from '../../domain/models/personal-category';
import { mapItemsToGenericListing } from '../generics/listing-result.mapper.ts';
import { GenericService } from './generic.service';

@Injectable({
  providedIn: 'root'
})
export class PersonalCategoryService extends GenericService {
  private controller = '/personalCategory/';
  constructor(private httpClient: HttpClient) {
    super(httpClient);
  }
  create(payload): Observable<ApiResult<PersonalCategoryDto>> {
    return this.post<ApiResult<PersonalCategoryDto>>(
    `${this.controller}`,
    payload,
    this.getAuthorizationHeader()
    );
  }
  update(id, payload): Observable<ApiResult<PersonalCategoryDto>> {
    return this.put<ApiResult<PersonalCategoryDto>>(
    `${this.controller}${id}`,
    payload,
    this.getAuthorizationHeader()
    );
  }
  remove(id): Observable<NullApiResult> {
    return this.delete<NullApiResult>(
    `${this.controller}${id}`
    );
  }

  getAll(): Observable<ApiResult<PersonalCategoryDto[]>> {
    return this.get<any>(
      `${this.controller}`,
      null,
      true
    );
  }
  getPersonalCategories(
    listingOption
  ): Observable<GenericListingResult<PersonalCategoryDto[]>> {
    const queryParams = this.setQueryParameters(listingOption);

    return this.get<any>(
      `${this.controller}PersonalCategorys?${queryParams}`,
      null,
      true
    ).pipe(
      map((res) =>
        mapItemsToGenericListing<PersonalCategoryDto[]>(res.data)
      )
    );
  }
  getPersonalCategory(id): Observable<ApiResult<PersonalCategoryDto>> {
    return this.get(`${this.controller}${id}`, null, true);
  }

  dropdown(
    dropdownListingOption
  ): Observable<GenericListingResult<PersonalCategoryFragment[]>> {
    const queryParams = this.setQueryParameters(
      dropdownListingOption
    );
    return this.get<any>(
      `${this.controller}Dropdown?${queryParams}`,
      null,
      true
    ).pipe(
      map((res) =>
        mapItemsToGenericListing<PersonalCategoryFragment[]>(
          res.data
        )
      )
    );
  }
}
