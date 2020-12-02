import { Injectable } from "@angular/core";
import { SubCategory } from './subcategory';

export class Category{
    categoryId:number
    categoryName:string
    description:string
    imageUrl:string
    dateCreated:Date
    subCategories:SubCategory[];
}