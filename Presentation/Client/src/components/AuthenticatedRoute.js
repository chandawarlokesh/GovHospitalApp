import React from "react";
import { Route, Redirect } from "react-router-dom";
import routes from "constants/routes";

const renderRoute = (children, component, render, routeProps) => {
    if (Array.isArray(children) && children.length === 0) {
        children = null;
    }

    if (typeof children === "function") {
        children = children(routeProps);

        if (children === undefined) {
            children = null;
        }
    }

    return children && React.Children.count(children) > 0
        ? children
        : routeProps.match
        ? component
            ? React.createElement(component, routeProps)
            : render
            ? render(routeProps)
            : null
        : null;
};

const AuthenticatedRoute = ({
    children,
    component,
    render,
    isAuthenticated,
    ...props
}) => {
    return (
        <Route
            {...props}
            render={(routeProps) => {
                if (
                    typeof isAuthenticated === "function" &&
                    isAuthenticated()
                ) {
                    return renderRoute(children, component, render, routeProps);
                }
                return <Redirect to={routes.login} />;
            }}
        />
    );
};

export default AuthenticatedRoute;
