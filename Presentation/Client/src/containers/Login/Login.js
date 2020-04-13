import React from "react";
import Logo from "components/Logo";
import { useForm } from "react-hook-form";
import { useHistory } from "react-router-dom";
import routes from "constants/routes";
import axios from "axios";
import { useData } from "containers/DataProvider";

const Login = () => {
    const history = useHistory();
    const { register, handleSubmit } = useForm();
    const { updatePatient } = useData();

    const onSubmit = ({ mobileNumber }) => {
        axios
            .get(`/api/patients/login/${mobileNumber}`)
            .then(({ data }) => {
                updatePatient({
                    id: data,
                });
                history.push(routes.home);
            })
            .catch(() => {
                updatePatient({ mobileNumber });
                history.push(routes.registration);
            });
    };

    return (
        <div className="container m-auto">
            <div className="w-full min-h-screen flex flex-col items-center justify-center p-4">
                <Logo className="p-4" />
                <div className="w-full max-w-md shadow-lg">
                    <h1 className="text-lg p-4 text-center font-bold bg-gray-200 uppercase">
                        {"Login"}
                    </h1>
                    <div className="flex flex-col p-6">
                        <form onSubmit={handleSubmit(onSubmit)}>
                            <input
                                type="tel"
                                placeholder="Mobile Number"
                                name="mobileNumber"
                                ref={register({
                                    required: true,
                                    minLength: 6,
                                    maxLength: 12,
                                })}
                                className="h-10 w-full mb-4 p-2 border border-gray-300"
                            />
                            <div className="flex justify-center">
                                <button
                                    type="submit"
                                    className="text-white bg-gray-500 px-3 py-2"
                                >
                                    {"Login"}
                                </button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Login;
