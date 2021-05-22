import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownConfig,BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import {TabsModule} from 'ngx-bootstrap/tabs';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { FileUploadModule } from 'ng2-file-upload';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
    }),
    TabsModule.forRoot(),
    NgxGalleryModule,
    FileUploadModule,
    BsDatepickerModule.forRoot(),
  ],
  exports:[CommonModule,
    BsDropdownModule,
    ToastrModule,
    TabsModule,
    NgxGalleryModule,
    FileUploadModule,
    BsDatepickerModule
  ]
})
export class SharedModule { }
