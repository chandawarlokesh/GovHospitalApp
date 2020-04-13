import React from "react";
import cx from "classnames";

const Logo = ({ className }) => {
    return (
        <div
            className={cx(
                "flex justify-center item-center text-lg font-bold",
                className
            )}
        >
            {"Government Hospital App"}
        </div>
    );
};

export default Logo;
