export interface TinyUrl {
  id: string;
  originalUrl: string;
  shortUrl: string;
  shortCode: string;
  isPrivate: boolean;
  clicks: number;
  createdAt: Date;
}
 
export interface CreateUrlRequest {
  originalUrl: string;
  isPrivate?: boolean;
}
 