import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { HttpEventType, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-productimport',
  templateUrl: './productimport.component.html',
  styleUrls: ['./productimport.component.css']
})
export class ProductimportComponent implements OnInit {
  baseUrl = environment.apiUrl + 'product/upload';
  public progress: number;
  public message: string;
  @Output() public onUploadFinished = new EventEmitter();
 
  constructor(private http: HttpClient) { }
 
  ngOnInit() {
  }
 
  public uploadFile = (files) => {
    if (files.length === 0) {
      return;
    }
 
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
 
    this.http.post(this.baseUrl, formData, {reportProgress: true, observe: 'events'})
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
          this.message = 'Upload success.';
          this.onUploadFinished.emit(event.body);
        }
      });
  }

}
