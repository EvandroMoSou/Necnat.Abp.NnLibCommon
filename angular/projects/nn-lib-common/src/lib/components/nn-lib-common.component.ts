import { Component, OnInit } from '@angular/core';
import { NnLibCommonService } from '../services/nn-lib-common.service';

@Component({
  selector: 'lib-nn-lib-common',
  template: ` <p>nn-lib-common works!</p> `,
  styles: [],
})
export class NnLibCommonComponent implements OnInit {
  constructor(private service: NnLibCommonService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
