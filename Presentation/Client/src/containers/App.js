import React from "react";
import {
    BrowserRouter as Router,
    Switch,
    Route,
    Redirect,
} from "react-router-dom";
import routes from "constants/routes";
import AuthenticatedRoute from "components/AuthenticatedRoute";
import { useData, DataProvider } from "./DataProvider";
import loadable from "@loadable/component";

export const AsyncComponent = loadable(({ name }) =>
    import(`./${name}/index.js`)
);

const App = () => {
    const { id } = useData();
    return (
        <Switch>
            <Route
                path={routes.login}
                component={() => <AsyncComponent name="Login" />}
            />
            <Route
                path={routes.registration}
                component={() => <AsyncComponent name="Registration" />}
            />
            <AuthenticatedRoute
                isAuthenticated={() => (id !== null && id !== "null") || !id}
                path={routes.home}
                component={() => <AsyncComponent name="Home" />}
            />
            <Route
                path={routes.admin}
                component={() => <AsyncComponent name="Admin" />}
            />
            <Redirect to={routes.home} />
            />
        </Switch>
    );
};

const AppContainer = () => (
    <Router>
        <DataProvider>
            <App />
        </DataProvider>
    </Router>
);

export default AppContainer;
