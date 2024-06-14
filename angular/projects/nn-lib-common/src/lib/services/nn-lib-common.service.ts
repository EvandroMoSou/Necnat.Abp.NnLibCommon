import { Injectable } from '@angular/core';
import { RestService } from '@abp/ng.core';

@Injectable({
  providedIn: 'root',
})
export class NnLibCommonService {
  apiName = 'NnLibCommon';

  constructor(private restService: RestService) {}

  sample() {
    return this.restService.request<void, any>(
      { method: 'GET', url: '/api/NnLibCommon/sample' },
      { apiName: this.apiName }
    );
  }
}
