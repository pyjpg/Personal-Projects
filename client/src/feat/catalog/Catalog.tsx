import { useEffect, useState } from "react";
import { Product } from "../../app/models/product";
import ProductList from "./ProductList";


export default function Catalog() {
    const [products, setProducts] = useState<Product[]>([]);

  useEffect(() => {
    fetch('http://localhost:5000/api/Products')
      .then(response => {
        if (!response.ok) {
          throw new Error(`HTTP error! Status: ${response.status}`);
        }
        return response.json();
      })
      .then(data => setProducts(data))
      .catch(error => console.error('Fetch error:', error));
  }, []);

 
    return (
        <>
            <ProductList products={products}/>
            
        </>
    ) 
}