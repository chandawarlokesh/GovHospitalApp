import React from "react";
import Logo from "components/Logo";
import RadioButton from "components/RadioButton";
import { useForm } from "react-hook-form";
import { useHistory } from "react-router-dom";
import routes from "constants/routes";
import { useData } from "containers/DataProvider";
import axios from "axios";

const Registration = () => {
    const history = useHistory();
    const { updatePatient, ...patient } = useData();
    const { register, handleSubmit } = useForm({
        defaultValues: patient,
    });

    const onSubmit = (requestData) => {
        axios
            .post(`/api/patients`, requestData)
            .then(({ data }) => {
                updatePatient(data);
                history.push(routes.home);
            })
            .catch((error) => {
                window.alert("something went wrong! try again.");
            });
    };

    return (
        <div className="container m-auto">
            <div className="w-full min-h-screen flex flex-col items-center justify-center p-4">
                <Logo className="p-4" />
                <div className="w-full max-w-md shadow-lg">
                    <h1 className="text-lg p-4 text-center font-bold bg-gray-200 uppercase">
                        {"Registration"}
                    </h1>
                    <div className="flex flex-col p-6">
                        <form onSubmit={handleSubmit(onSubmit)}>
                            <input
                                type="text"
                                placeholder="Name"
                                name="Name"
                                ref={register({
                                    required: true,
                                    maxLength: 255,
                                })}
                                className="h-10 w-full mb-4 p-2 border border-gray-300"
                            />
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
                            <input
                                type="datetime-local"
                                placeholder="Date Of Birth"
                                name="dateOfBirth"
                                ref={register({ required: true })}
                                className="h-10 w-full mb-4 p-2 border border-gray-300"
                            />
                            <select
                                name="gender"
                                ref={register()}
                                className="h-10 w-full mb-4 p-2 border border-gray-300 "
                                placeholder="Select a Gender..."
                            >
                                <option value={0}>{"Male"}</option>
                                <option value={1}>{"Female"}</option>
                                <option value={2}>{"Other"}</option>
                            </select>
                            <input
                                type="text"
                                placeholder="Street"
                                name="address.street"
                                ref={register({
                                    required: true,
                                    maxLength: 255,
                                })}
                                className="h-10 w-full mb-4 p-2 border border-gray-300"
                            />
                            <div className="flex flex-row">
                                <input
                                    type="text"
                                    placeholder="City"
                                    name="address.city"
                                    ref={register({
                                        required: true,
                                        maxLength: 80,
                                    })}
                                    className="h-10 w-full mb-4 p-2 border border-gray-300 mr-4"
                                />
                                <input
                                    type="text"
                                    placeholder="State"
                                    name="address.state"
                                    ref={register({
                                        required: true,
                                        maxLength: 50,
                                    })}
                                    className="h-10 w-full mb-4 p-2 border border-gray-300 mr-4"
                                />
                                <input
                                    type="text"
                                    placeholder="ZipCode"
                                    name="address.zipCode"
                                    ref={register({
                                        required: true,
                                        maxLength: 6,
                                    })}
                                    className="h-10 w-full mb-4 p-2 border border-gray-300 "
                                />
                            </div>
                            <div className="flex justify-center">
                                <button
                                    type="submit"
                                    className="text-white bg-gray-500 px-3 py-2"
                                >
                                    {"Register"}
                                </button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    );
};
export default Registration;
