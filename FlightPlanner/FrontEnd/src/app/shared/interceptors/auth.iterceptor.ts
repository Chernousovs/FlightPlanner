import { Injectable } from "@angular/core";
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { catchError, Observable, throwError } from "rxjs";
import { ErrorService } from "src/app/services/error.service";

@Injectable({
  providedIn: 'root'
})
export class AuthInterceptor implements HttpInterceptor {
  constructor(private errorService: ErrorService) {  }

  intercept(req: HttpRequest<any>, next : HttpHandler): Observable<HttpEvent<any>> {
    const admin = localStorage.getItem('admin');
    const shouldAddAuthData = req.headers.get('Add-Auth-Data') !== null;
    if (admin && shouldAddAuthData)
    {
      const authorizationData = 'Basic ' + btoa(admin);

      req = req.clone({
        headers: req.headers.delete('Add-Auth-Data'),
        setHeaders: { Authorization: authorizationData }
      });
    }

    return next.handle(req).pipe(
      catchError(error => {
        this.errorService.handleError(error)
        return throwError(error)
      }));
  }
}
