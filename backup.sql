--
-- PostgreSQL database dump
--

-- Dumped from database version 17.2 (Debian 17.2-1.pgdg120+1)
-- Dumped by pg_dump version 17.2 (Debian 17.2-1.pgdg120+1)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: comment; Type: TABLE; Schema: public; Owner: user
--

CREATE TABLE public.comment (
    id character varying(36) NOT NULL,
    task_id character varying(36),
    description text,
    is_active boolean DEFAULT true,
    is_deleted boolean DEFAULT false,
    creation_date timestamp with time zone DEFAULT CURRENT_TIMESTAMP,
    create_by_user character varying(36),
    modification_date timestamp with time zone,
    update_by_user character varying(36)
);


ALTER TABLE public.comment OWNER TO "user";

--
-- Name: history; Type: TABLE; Schema: public; Owner: user
--

CREATE TABLE public.history (
    id character varying(36) NOT NULL,
    task_id character varying(36),
    change_details text,
    is_active boolean DEFAULT true,
    is_deleted boolean DEFAULT false,
    creation_date timestamp with time zone DEFAULT CURRENT_TIMESTAMP,
    create_by_user character varying(36),
    modification_date timestamp with time zone,
    update_by_user character varying(36)
);


ALTER TABLE public.history OWNER TO "user";

--
-- Name: profiles; Type: TABLE; Schema: public; Owner: user
--

CREATE TABLE public.profiles (
    id character varying(36) NOT NULL,
    name character varying(255),
    is_active boolean DEFAULT true,
    is_deleted boolean DEFAULT false,
    creation_date timestamp with time zone DEFAULT CURRENT_TIMESTAMP
);


ALTER TABLE public.profiles OWNER TO "user";

--
-- Name: project; Type: TABLE; Schema: public; Owner: user
--

CREATE TABLE public.project (
    id character varying(36) NOT NULL,
    name character varying(255),
    description text,
    is_active boolean DEFAULT true,
    is_deleted boolean DEFAULT false,
    creation_date timestamp with time zone DEFAULT CURRENT_TIMESTAMP,
    create_by_user character varying(36),
    modification_date timestamp with time zone,
    update_by_user character varying(36)
);


ALTER TABLE public.project OWNER TO "user";

--
-- Name: task; Type: TABLE; Schema: public; Owner: user
--

CREATE TABLE public.task (
    id character varying(36) NOT NULL,
    project_id character varying(36),
    title character varying(255),
    description text,
    status character varying(50),
    priority character varying(50),
    is_active boolean DEFAULT true,
    is_deleted boolean DEFAULT false,
    creation_date timestamp with time zone DEFAULT CURRENT_TIMESTAMP,
    create_by_user character varying(36),
    modification_date timestamp with time zone,
    update_by_user character varying(36)
);


ALTER TABLE public.task OWNER TO "user";

--
-- Name: user; Type: TABLE; Schema: public; Owner: user
--

CREATE TABLE public."user" (
    id character varying(36) NOT NULL,
    profile_id character varying(36) NOT NULL,
    first_name character varying(255) NOT NULL,
    last_name character varying(255) NOT NULL,
    email character varying(255) NOT NULL,
    is_active boolean DEFAULT true NOT NULL,
    is_deleted boolean DEFAULT false NOT NULL,
    creation_date timestamp with time zone DEFAULT CURRENT_TIMESTAMP NOT NULL,
    create_by_user character varying(36) NOT NULL,
    modification_date timestamp with time zone,
    update_by_user character varying(36)
);


ALTER TABLE public."user" OWNER TO "user";

--
-- Data for Name: comment; Type: TABLE DATA; Schema: public; Owner: user
--

COPY public.comment (id, task_id, description, is_active, is_deleted, creation_date, create_by_user, modification_date, update_by_user) FROM stdin;
\.


--
-- Data for Name: history; Type: TABLE DATA; Schema: public; Owner: user
--

COPY public.history (id, task_id, change_details, is_active, is_deleted, creation_date, create_by_user, modification_date, update_by_user) FROM stdin;
6f8b30ae-bf59-4a82-ac6d-5e22539a9e81	123e4567-e89b-12d3-a456-426655440000	Titulo: Tarea 1 -> Tarea 1.100; Descripción: Descripción detallada de la tarea 1. -> Descripción detallada de la tarea 1.100.	t	f	2025-01-20 10:05:34+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
\.


--
-- Data for Name: profiles; Type: TABLE DATA; Schema: public; Owner: user
--

COPY public.profiles (id, name, is_active, is_deleted, creation_date) FROM stdin;
550e8400-e29b-41d4-a716-446655440000	usuario	t	f	2025-01-19 18:20:57+00
550e8400-e29b-41d4-a716-446655440001	manager	t	f	2025-01-19 18:20:57+00
550e8400-e29b-41d4-a716-446655440002	admin	t	f	2025-01-19 18:20:57+00
\.


--
-- Data for Name: project; Type: TABLE DATA; Schema: public; Owner: user
--

COPY public.project (id, name, description, is_active, is_deleted, creation_date, create_by_user, modification_date, update_by_user) FROM stdin;
0970d093-a488-4d98-affe-8f1fa4e4ddbf	projeto inicial testes	alguma coisa aqui	t	f	2025-01-19 20:43:17+00	123e4567-e89b-12d3-a456-426655440000	2025-01-20 04:11:22+00	123e4567-e89b-12d3-a456-426655440000
\.


--
-- Data for Name: task; Type: TABLE DATA; Schema: public; Owner: user
--

COPY public.task (id, project_id, title, description, status, priority, is_active, is_deleted, creation_date, create_by_user, modification_date, update_by_user) FROM stdin;
123e4567-e89b-12d3-a456-426655440000	0970d093-a488-4d98-affe-8f1fa4e4ddbf	Tarea 1.100	Descripción detallada de la tarea 1.100.	Pendente	Alta	t	f	2025-01-20 03:00:00+00	123e4567-e89b-12d3-a456-426655440000	2025-01-20 10:05:23+00	123e4567-e89b-12d3-a456-426655440000
\.


--
-- Data for Name: user; Type: TABLE DATA; Schema: public; Owner: user
--

COPY public."user" (id, profile_id, first_name, last_name, email, is_active, is_deleted, creation_date, create_by_user, modification_date, update_by_user) FROM stdin;
123e4567-e89b-12d3-a456-426655440000	550e8400-e29b-41d4-a716-446655440000	Juan	Perez	juan.perez@example.com	t	f	2025-01-19 18:32:12+00	550e8400-e29b-41d4-a716-446655440002	\N	\N
123e4567-e89b-12d3-a456-426655440001	550e8400-e29b-41d4-a716-446655440001	Maria	Lopez	maria.lopez@example.com	t	f	2025-01-19 18:32:12+00	550e8400-e29b-41d4-a716-446655440002	\N	\N
123e4567-e89b-12d3-a456-426655440002	550e8400-e29b-41d4-a716-446655440002	Carlos	Gomez	carlos.gomez@example.com	t	f	2025-01-19 18:32:12+00	550e8400-e29b-41d4-a716-446655440002	\N	\N
\.


--
-- Name: comment comment_pkey; Type: CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.comment
    ADD CONSTRAINT comment_pkey PRIMARY KEY (id);


--
-- Name: history history_pkey; Type: CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.history
    ADD CONSTRAINT history_pkey PRIMARY KEY (id);


--
-- Name: profiles profiles_pkey; Type: CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.profiles
    ADD CONSTRAINT profiles_pkey PRIMARY KEY (id);


--
-- Name: project project_pkey; Type: CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.project
    ADD CONSTRAINT project_pkey PRIMARY KEY (id);


--
-- Name: task task_pkey; Type: CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.task
    ADD CONSTRAINT task_pkey PRIMARY KEY (id);


--
-- Name: user user_pkey; Type: CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public."user"
    ADD CONSTRAINT user_pkey PRIMARY KEY (id);


--
-- Name: comment comment_task_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.comment
    ADD CONSTRAINT comment_task_id_fkey FOREIGN KEY (task_id) REFERENCES public.task(id);


--
-- Name: history history_task_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.history
    ADD CONSTRAINT history_task_id_fkey FOREIGN KEY (task_id) REFERENCES public.task(id);


--
-- Name: task task_project_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.task
    ADD CONSTRAINT task_project_id_fkey FOREIGN KEY (project_id) REFERENCES public.project(id);


--
-- Name: user user_profile_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public."user"
    ADD CONSTRAINT user_profile_id_fkey FOREIGN KEY (profile_id) REFERENCES public.profiles(id);


--
-- PostgreSQL database dump complete
--

