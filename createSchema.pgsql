-- Database: CadU

-- DROP DATABASE "CadU";

CREATE DATABASE "CadU"
    WITH 
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'en_US.utf8'
    LC_CTYPE = 'en_US.utf8'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1;

COMMENT ON DATABASE "CadU"
    IS 'Projeto CadU';

-- Extension: "uuid-ossp"

-- DROP EXTENSION "uuid-ossp";

CREATE EXTENSION "uuid-ossp"
    SCHEMA public
    VERSION "1.1";
    
-- Table: public.User

-- DROP TABLE public."User";

CREATE TABLE public."User"
(
    "Id" uuid NOT NULL,
    "Name" character varying(60) COLLATE pg_catalog."default" NOT NULL,
    "Password" character varying(20) COLLATE pg_catalog."default" NOT NULL,
    "Email" character varying(60) COLLATE pg_catalog."default",
    "Role" character varying(10) COLLATE pg_catalog."default",
    "IsAdmin" boolean NOT NULL,
    CONSTRAINT "Pessoa_pkey" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE public."User"
    OWNER to postgres;