import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { NnLibCommonComponent } from './components/nn-lib-common.component';
import { NnLibCommonService } from '@necnat.Abp/nn-lib-common';
import { of } from 'rxjs';

describe('NnLibCommonComponent', () => {
  let component: NnLibCommonComponent;
  let fixture: ComponentFixture<NnLibCommonComponent>;
  const mockNnLibCommonService = jasmine.createSpyObj('NnLibCommonService', {
    sample: of([]),
  });
  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [NnLibCommonComponent],
      providers: [
        {
          provide: NnLibCommonService,
          useValue: mockNnLibCommonService,
        },
      ],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NnLibCommonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
