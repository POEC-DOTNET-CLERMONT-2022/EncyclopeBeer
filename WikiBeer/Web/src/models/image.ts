import { Byte } from "@angular/compiler/src/util";
import { reduce } from "rxjs";

export class Image{

  private _byteImage: Byte[]

  constructor(byteImage: Byte[]){
    this._byteImage = byteImage
  }

  get byteImage(): Byte[] {return this._byteImage}
  set byteImage(value: Byte[]) {this._byteImage = value}

}
