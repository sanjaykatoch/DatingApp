import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownConfig,BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
    })
  ],
  exports:[CommonModule,
    BsDropdownModule,
    ToastrModule
  ]
})
export class SharedModule { }
