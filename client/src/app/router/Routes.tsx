import { Navigate, createBrowserRouter } from "react-router-dom";
import App from "../layout/App";
import HomePage from "../../feat/home/HomePage";
import AboutPage from "../../feat/about/AboutPage";
import ContactPage from "../../feat/contact/ContactPage";
import ProductDetails from "../../feat/catalog/ProductDetails";
import Catalog from "../../feat/catalog/Catalog";
import ServerError from "../errors/ServerError";
import NotFound from "../errors/NotFound";
import BasketPage from "../../feat/basket/BasketPage";
import CheckoutPage from "../../feat/checkout/CheckoutPage";

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
            {path:'server-error', element: <ServerError/>},
            {path:'not-found', element: <NotFound/>},
            {path:'basket', element: <BasketPage/>},
            {path:'checkout', element: <CheckoutPage/>},
            {path:'*', element: <Navigate replace to='/not-found'/>}
        ]
    }
])