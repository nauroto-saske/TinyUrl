import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CreateUrlRequest, TinyUrl } from './models/tiny-url';
import { TinyUrlService } from './services/tiny-url.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, FormsModule, HttpClientModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
   title = 'Tiny URL';
  originalUrl = '';
  isPrivate = false;
  urls: TinyUrl[] = [];
  searchQuery = '';
  loading = false;
  message = '';
  messageType: 'success' | 'error' = 'success';

  constructor(private tinyUrlService: TinyUrlService) { }
 
  ngOnInit(): void {
    this.loadPublicUrls();
  }
 
  loadPublicUrls(){
    this.loading = true;
    this.tinyUrlService.getPublicUrls().subscribe({
      next: (urls) => {
        this.urls = urls;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading URLs:', error);
        this.showMessage('Failed to load URLs', 'error');
        this.loading = false;
      }
    });
  }
 
  generateUrl(){
    if (!this.originalUrl.trim()) {
      this.showMessage('Please enter a URL', 'error');
      return;
    }
 
    // Basic URL validation
    if (!this.isValidUrl(this.originalUrl)) {
      this.showMessage('Please enter a valid URL', 'error');
      return;
    }
 
    const request: CreateUrlRequest = {
      originalUrl: this.originalUrl,
      isPrivate: this.isPrivate
    };
 
    this.loading = true;
    this.tinyUrlService.createUrl(request).subscribe({
      next: (url) => {
        this.showMessage('Short URL created successfully!', 'success');
        if (!url.isPrivate) {
          this.urls.unshift(url);
        }
        this.originalUrl = '';
        this.isPrivate = false;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error creating URL:', error);
        this.showMessage('Failed to create short URL', 'error');
        this.loading = false;
      }
    });
  }
 
  searchUrls() {
    if (!this.searchQuery.trim()) {
      this.loadPublicUrls();
      return;
    }
 
    this.loading = true;
    this.tinyUrlService.searchUrls(this.searchQuery).subscribe({
      next: (urls) => {
        this.urls = urls;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error searching URLs:', error);
        this.showMessage('Failed to search URLs', 'error');
        this.loading = false;
      }
    });
  }
 
  deleteUrl(shortCode: string) {
    if (!confirm('Are you sure you want to delete this URL?')) {
      return;
    }
 
    this.loading = true;
    this.tinyUrlService.deleteUrl(shortCode).subscribe({
      next: () => {
        this.showMessage('URL deleted successfully', 'success');
        this.urls = this.urls.filter(u => u.shortCode !== shortCode);
        this.loading = false;
      },
      error: (error) => {
        console.error('Error deleting URL:', error);
        this.showMessage('Failed to delete URL', 'error');
        this.loading = false;
      }
    });
  }
 
  async copyUrl(shortUrl: string): Promise<void> {
    try {
      await this.tinyUrlService.copyToClipboard(shortUrl);
      this.showMessage('Copied to clipboard!', 'success');
    } catch (error) {
      console.error('Error copying to clipboard:', error);
      this.showMessage('Failed to copy to clipboard', 'error');
    }
  }
 
  private isValidUrl(url: string): boolean {
    try {
      new URL(url);
      return true;
    } catch {
      return false;
    }
  }
 
  private showMessage(message: string, type: 'success' | 'error'): void {
    this.message = message;
    this.messageType = type;
    setTimeout(() => {
      this.message = '';
    }, 3000);
  }
 
  clearSearch(): void {
    this.searchQuery = '';
    this.loadPublicUrls();
  }
}
