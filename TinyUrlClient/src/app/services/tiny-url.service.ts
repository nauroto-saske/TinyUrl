import { Injectable } from '@angular/core';
import { CreateUrlRequest, TinyUrl } from '../models/tiny-url';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environment ';

@Injectable({
  providedIn: 'root'
})
export class TinyUrlService {
  private apiUrl = environment.apiUrl;
 
  constructor(private http: HttpClient) { }
 
  
  createUrl(request: CreateUrlRequest): Observable<TinyUrl> {
    return this.http.post<TinyUrl>(`${this.apiUrl}/api/url/add`, request);
  }
 
  getAllUrls(): Observable<TinyUrl[]> {
    return this.http.get<TinyUrl[]>(`${this.apiUrl}/api/url/all`);
  }
 
  getPublicUrls(): Observable<TinyUrl[]> {
    return this.http.get<TinyUrl[]>(`${this.apiUrl}/api/url/public`);
  }
 
  searchUrls(query: string): Observable<TinyUrl[]> {
    return this.http.get<TinyUrl[]>(`${this.apiUrl}/api/url/search?query=${encodeURIComponent(query)}`);
  }
 
  deleteUrl(shortCode: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/api/url/delete/${shortCode}`);
  }
 
  copyToClipboard(text: string): Promise<void> {
    return navigator.clipboard.writeText(text);
  }
}
