import { ModuleWithProviders, NgModule } from '@angular/core';
import { NN_LIB_COMMON_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class NnLibCommonConfigModule {
  static forRoot(): ModuleWithProviders<NnLibCommonConfigModule> {
    return {
      ngModule: NnLibCommonConfigModule,
      providers: [NN_LIB_COMMON_ROUTE_PROVIDERS],
    };
  }
}
