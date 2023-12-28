import { createBrowserRouter } from "react-router-dom";
import App from "../layout/App";
import HomePage from "../../feat/home/HomePage";
import AboutPage from "../../feat/about/AboutPage";
import ContactPage from "../../feat/contact/ContactPage";
import ProductDetails from "../../feat/catalog/ProductDetails";
import Catalog from "../../feat/catalog/Catalog";

export const router = createBrowserRouter([
    {
        path:'/',
        element: <App />,
        children: [
            {path:'', element: <HomePage/>},
            {path:'about', element: <AboutPage/>},
            {path:'contact', element: <ContactPage/>},
            {path:'catalog', element: <Catalog/>},
            {path:'catalog/:id', element: <ProductDetails/>},
        ]
    }
])