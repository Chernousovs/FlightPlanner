import { HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ToastrService } from "ngx-toastr";

@Injectable({
  providedIn: 'root'
})
export class ErrorService {
  constructor(private toastr: ToastrService) {
  }

  handleError(error: HttpErrorResponse): void {
    const message = error.error instanceof ErrorEvent ? error.error.message : error.message;
    this.toastr.error(message);
  }
}
