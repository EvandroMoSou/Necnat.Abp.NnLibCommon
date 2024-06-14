import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { NnLibCommonComponent } from './components/nn-lib-common.component';
import { NnLibCommonRoutingModule } from './nn-lib-common-routing.module';

@NgModule({
  declarations: [NnLibCommonComponent],
  imports: [CoreModule, ThemeSharedModule, NnLibCommonRoutingModule],
  exports: [NnLibCommonComponent],
})
export class NnLibCommonModule {
  static forChild(): ModuleWithProviders<NnLibCommonModule> {
    return {
      ngModule: NnLibCommonModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<NnLibCommonModule> {
    return new LazyModuleFactory(NnLibCommonModule.forChild());
  }
}
