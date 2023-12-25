import React, { useEffect, useState } from "react";
import { Product } from "../models/product";
import Catalog from "../../feat/catalog/Catalog";

function App() {
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

  function addProduct() {
    setProducts(prevState => [
      ...prevState,
      {
        id: prevState.length + 1, // Incrementing id
        name: 'product' + (prevState.length + 1),
        price: (prevState.length * 100) + 100,
        brand: 'Armani',
        description: 'Best product description ever',
        pictureUrl: 'http://picsum.photos/200'
      }
    ]);
  }

  return (
    <div>
      <h1>Re-Store</h1>
      <Catalog products={products} addProduct={addProduct}/>
      
    </div>
  );
}

export default App;
