import { TestBed } from '@angular/core/testing';
import { NnLibCommonService } from './services/nn-lib-common.service';
import { RestService } from '@abp/ng.core';

describe('NnLibCommonService', () => {
  let service: NnLibCommonService;
  const mockRestService = jasmine.createSpyObj('RestService', ['request']);
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        {
          provide: RestService,
          useValue: mockRestService,
        },
      ],
    });
    service = TestBed.inject(NnLibCommonService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
