import { useEffect, useState } from "react";
import { Product } from "../../app/models/product";
import ProductList from "./ProductList";
import agent from "../../app/api/agent";
import LoadingComponent from "../../app/layout/LoadingComponent";

export default function Catalog() {
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);


  useEffect(() => {
    agent.Catalog.list()
      .then((products) => {
        console.log("Fetched products:", products);
        if (!Array.isArray(products)) {
          console.error("Fetched data is not an array:", products);
          return;
        }
        setProducts(products);
      })
      .catch((error) => {
        console.error('Error fetching data:', error);
      })
      .finally(() => setLoading(false));
  }, []);
  

  if (loading) return <LoadingComponent message='Loading products...'/>

  return (
    <>
      <ProductList products={products}/>
    </>
  );
}
