import { Injectable } from '@angular/core';
import * as CryptoJS from 'crypto-js';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EncryptionService {
  private key = CryptoJS.enc.Utf8.parse(environment.EncryptKey);
  constructor() { }

  encrypt(value: string): string {
    return CryptoJS.AES.encrypt(value, this.key)
      .toString();
  }

  decrypt(textToDecrypt: string) {
    return CryptoJS.AES.decrypt(textToDecrypt, this.key)
      .toString(CryptoJS.enc.Utf8);
  }
}